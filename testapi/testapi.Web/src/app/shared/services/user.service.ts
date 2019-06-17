import { Injectable } from '@angular/core';

import { ConfigService } from './config.service';
import { User, Config } from '../models';

@Injectable()
export class UserService {

  constructor(
    private configService: ConfigService
  ) {
  }

  user(): User {
    return this.configService.config.user;
  }

  isLoggedIn(): boolean {
    return this.configService.config.user != null;
  }

  userHasPermission(permissions: string[]): boolean {
    if (this.isLoggedIn() && this.configService.config.user.roles) {
      for (let j = 0; j < permissions.length; j++) {
        for (let i = 0; i < this.configService.config.user.roles.length; i++) {
          if (permissions[j] === this.configService.config.user.roles[i]) {
            return true;
          }
        }
      }
    }
    return false;
  }

  login() {
    window.location.href = `${this.configService.config.appRoot}support/login`;
  }
}
