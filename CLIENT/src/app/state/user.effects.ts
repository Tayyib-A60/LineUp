import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Action } from '@ngrx/store';
import { UserService } from './user.service';
import { UserActionTypes } from './user.action.types';
import * as userActions from '../state/user.actions';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { User } from '../spaces/models/user.model';

@Injectable()
export class UserEffects {

    constructor(private action$: Actions,
                private userService: UserService) { }
    
    @Effect()
    createUser$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.CreateUser),
        map((action: userActions.CreateUser) => action.payload),
        mergeMap((user: User) => 
            this.userService.createUserAsCustomer(user).pipe(
                map(response => new userActions.CreateUserSuccess(response)),
                catchError(err => of(new userActions.CreateUserFailure(err))
                )
            )
        )
    );

    @Effect()
    createMerchantUser$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.CreateMerchantUser),
        map((action: userActions.CreateMerchantUser) => action.payload),
        mergeMap((user: User) => 
            this.userService.createUserAsMerchant(user).pipe(
                map(response => new userActions.CreateUserSuccess(response)),
                catchError(err => of(new userActions.CreateUserFailure(err))
                )
            )
        )
    );

    @Effect()
    signInUser$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.SignInUser),
        map((action: userActions.SignInUser) => action.payload),
        mergeMap((user: User) => 
            this.userService.signInUser(user).pipe(
                map(user => new userActions.SignInUserSuccess(user)),
                catchError(err => of(new userActions.SignInUserFailure(err))
                )
            )
        )
    );

    @Effect()
    forgotPassword$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.ForgotPassword),
        map((action: userActions.ForgotPassword) => action.payload),
        mergeMap((user: any) => 
            this.userService.forgotPassword(user).pipe(
                map(res => new userActions.ForgotPasswordSuccess(res)),
                catchError(err => of(new userActions.ForgotPasswordFailure(err)))
            )
        )
    );

    @Effect()
    resetPassword$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.ResetPassword),
        map((action: userActions.ResetPassword) => action.payload),
        mergeMap((userToUpdate: any) => 
            this.userService.resetPassword(userToUpdate).pipe(
                map(res => new userActions.ResetPasswordSuccess(res)),
                catchError(err => of(new userActions.ResetPasswordFailure(err)))
            )
        )
    );

    @Effect()
    confirmEmail$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.ConfirmEmail),
        map((action: userActions.ConfirmEmail) => action.payload),
        mergeMap((userToConfirm: any) =>
            this.userService.confirmEmail(userToConfirm).pipe(
                map(res => new userActions.ConfirmEmailSuccess(res.toString())),
                catchError(err => of(new userActions.ConfirmEmailFailure(err)))
            )
        )
    );
    @Effect()
    confirmAsMerchant$: Observable<Action> = this.action$.pipe(
        ofType(UserActionTypes.ConfirmAsMerchant),
        map((action: userActions.ConfirmAsMerchant) => action.payload),
        mergeMap((merchantToConfirm: any) =>
            this.userService.confirmAsMerchant(merchantToConfirm).pipe(
                map(res => new userActions.ConfirmAsMerchantSuccess(res.toString())),
                catchError(err => of(new userActions.ConfirmAsMerchantFailure(err)))
            )
        )
    );



}