import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './spaces/admin/admin.component';
import { HomeComponent } from './home/home.component';
import { AddspaceComponent } from './spaces/addspace/addspace.component';
import { ManagespacesComponent } from './spaces/managespaces/managespaces.component';
import { ManagereservationsComponent } from './spaces/managereservations/managereservations.component';
import { ManagebookingsComponent } from './spaces/managebookings/managebookings.component';
import { ManageenquiriesComponent } from './spaces/manageenquiries/manageenquiries.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ListYourSpaceComponent } from './list-your-space/list-your-space.component';
import { ProfileComponent } from './profile/profile.component';
import { BookingRequestComponent } from './booking-request/booking-request.component';
import { BookingRequestTwoComponent } from './booking-request-two/booking-request-two.component';
import { SpaceDisplayComponent } from './space-display/space-display.component';
import { AddPropsComponent } from './spaces/add-props/add-props.component';
import { MerchantAuthGuardService } from './services/merchantAuthGuardService';
import { ResetPasswordComponent } from './reset-password/reset-password.component';


const routes: Routes = [
  { path: 'admin', component: AdminComponent, canActivate: [ MerchantAuthGuardService ], children: [
    { path: 'add-space', component: AddspaceComponent },
    { path: 'edit-space/:id', component: AddspaceComponent },
    { path: 'manage-space', component: ManagespacesComponent },
    { path: 'manage-reservations', component: ManagereservationsComponent },
    { path: 'manage-bookings', component: ManagebookingsComponent },
    { path: 'manage-enquiries', component: ManageenquiriesComponent },
    { path: 'create-addons', component: AddPropsComponent }
  ]},
  { path: '', component: HomeComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'sign-up-as-merchant', component: SignUpComponent },
  { path: 'forgot-password', component: ResetPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'confirm-email', component: ResetPasswordComponent },
  { path: 'list-your-space', component: ListYourSpaceComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'booking-request', component: BookingRequestComponent },
  { path: 'booking-request-two', component: BookingRequestTwoComponent },
  { path: 'space-display', component: SpaceDisplayComponent },
  { path: '**', redirectTo: '' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
