import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

import { Config, User, Refresh } from '../models';

@Injectable()
export class ConfigService {
  private sessionStorageName = 'Template_user';
  private configSubject = new Subject<Config>();
  public config: Config;
  public config$ = this.configSubject.asObservable();
  constructor(private http: HttpClient) {
    this.populate();
  }

  // populates the config
  // This runs once on application startup
  populate() {
    let config = <Config>window['config'];
    if (!config.user) {
      const temp = sessionStorage.getItem(this.sessionStorageName);
      if (temp) {
        config = JSON.parse(temp);
      }
    } else {
      sessionStorage.setItem(this.sessionStorageName, JSON.stringify(config));
    }
    this.config = config;
    this.configSubject.next(config);
  }

  setConfig(config: Config) {
    sessionStorage.setItem(this.sessionStorageName, JSON.stringify(config));
    this.configSubject.next(config);
    this.config = config;
  }

  getConfig(): Config {
    return this.config;
  }

  hasToken(): boolean {
    return this.config.authToken !== null;
  }

  hasRefreshToken(): boolean {
    return this.config.refreshToken !== null;
  }

  setUser(user: User) {
    const config = this.config;
    config.user = user;

    this.setConfig(config);
  }

  refreshAuthToken(): Observable<Refresh> {
    return this.http.get<Refresh>(`support/refresh`);
  }
}
