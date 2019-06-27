import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserService } from '../services/user.service';

@Injectable()
export class AuthorizeGuard implements CanActivate {

    constructor(private userService: UserService) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.userService.isLoggedIn()) {
            // if we're requiring a role, check for it
            if (next.data['roles']) {
                if (this.userService.userHasPermission(next.data['roles'])) {
                    return true;
                }

                // you probably want to navigate to an access denied page or to your "home" page
                return false;
            }

            return true; // no roles required, just authentication checked
        }

        // if you allow anonymous access, they're not logged in yet, you probably want to redirect them to log in
        return false;
    }
}
