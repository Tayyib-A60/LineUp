import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';

import * as userReducer from '../state/user.reducers';
import * as userActions from '../state/user.actions';

@Component({
  selector: 'app-new-nav',
  templateUrl: './new-nav.component.html',
  styleUrls: ['./new-nav.component.scss']
})
export class NewNavComponent implements OnInit {
  currentUser: any;

  constructor(private store: Store<userReducer.UserState>,
    private router: Router) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  merchantIsAuthenticated() {
    if(this.currentUser) {      
      return this.currentUser['roles'] === 'Merchant' || 'AnySpaces'? true: false;
    }    
  }

  signOut() {
    localStorage.removeItem('currentUser');
    this.currentUser = null;
    this.store.dispatch(new userActions.SignOutUser());
    this.router.navigate(['']);
  }

  anyUserIsAuthenticated() {
    return localStorage.getItem('currentUser')? true: false;
  }

}
