import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
})

export class MerchantAuthGuardService implements CanActivate {

    constructor(private router: Router) { }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = localStorage.getItem('currentUser');
        
        if(JSON.parse(currentUser)['roles'] === 'Merchant' || 'AnySpaces') {
            return true;
        }
        this.router.navigate(['/'], {queryParams: {returnUrl: state.url}});
        return false;
      }
    
      isAuthenticated(): boolean {
        return localStorage.getItem('currentUser') ? true : false;
      }
}