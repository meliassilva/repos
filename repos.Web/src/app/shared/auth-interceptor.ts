import {
  throwError as observableThrowError,
  BehaviorSubject,
  Observable
} from 'rxjs';
import { catchError, filter, take, switchMap, finalize } from 'rxjs/operators';
import { Injectable, Injector } from '@angular/core';

import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { ConfigService } from './services';
import { Config, Refresh } from './models';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  config: Config;
  isRefreshingToken = false;
  tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  configService: ConfigService;
  constructor(private injector: Injector) {}

  addToken(req: HttpRequest<any>): HttpRequest<any> {
    const headers = {};
    if (this.config.authToken) {
      headers['Authorization'] = `Bearer ${this.config.authToken}`;
    }
    return req.clone({
      setHeaders: headers,
      url: req.url.startsWith('api')
        ? req.url.replace('api/', this.config.apiUrl)
        : req.url
    });
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.configService = this.injector.get(ConfigService);
    this.config = this.configService.getConfig();

    request = this.addToken(request);
    return next.handle(request).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            return this.refreshToken(request, next);
          }
        }
        return observableThrowError(err);
      })
    );
  }

  refreshToken(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;

      // Reset here so that the following requests wait until the token
      // comes back from the refreshToken call.
      this.tokenSubject.next(null);

      return this.configService.refreshAuthToken().pipe(
        switchMap((refresh: Refresh) => {
          if (refresh && refresh.token) {
            this.config.authToken = refresh.token;
            this.configService.setConfig(this.config);
            this.tokenSubject.next(refresh.token);
            return next.handle(this.addToken(req));
          }
          // If we don't get a new token, we are in trouble so logout.
          return this.login();
        }),
        catchError(error => {
          // If there is an exception calling 'refreshToken', bad news so logout.
          return this.login();
        }),
        finalize(() => {
          this.isRefreshingToken = false;
        })
      );
    } else {
      return this.tokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(token => {
          return next.handle(this.addToken(req));
        })
      );
    }
  }

  private login() {
    window.location.href =
      this.config.appRoot + 'support/login?redirectUrl=' + window.location.href;
    return observableThrowError('');
  }
}
