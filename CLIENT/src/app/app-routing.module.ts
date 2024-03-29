import { ImageEditorComponent } from './spaces/image-editor/image-editor.component';
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
import { ManageMerchantsComponent } from './spaces/manage-merchants/manage-merchants.component';
import { MapSpaceComponent } from './map-space/map-space.component';
import { SpaceHomeComponent } from './space-home/space-home.component';
import { NewSpaceComponent } from './new-space/new-space.component';
import { SampleCarouselComponent } from './sample-carousel/sample-carousel.component';


const routes: Routes = [
  { path: 'admin', component: AdminComponent, canActivate: [ MerchantAuthGuardService ], children: [
    { path: 'add-space', component: AddspaceComponent },
    { path: 'edit-space/:id', component: AddspaceComponent },
    { path: 'manage-space', component: ManagespacesComponent },
    { path: 'upload-images/:id', component: ImageEditorComponent},
    { path: 'manage-reservations', component: ManagereservationsComponent },
    { path: 'manage-bookings', component: ManagebookingsComponent },
    { path: 'manage-enquiries', component: ManageenquiriesComponent },
    { path: 'create-addons', component: AddPropsComponent },
    { path: 'merchants', component: ManageMerchantsComponent },
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
  { path: 'map-space/:spaceType', component: MapSpaceComponent },
  { path: 'map-space', component: MapSpaceComponent },
  { path: 'space-home', component: SpaceHomeComponent },
  { path: 'new-space/:id', component: NewSpaceComponent },
  { path: 'space-details/:id', component: SpaceDisplayComponent },
  { path: 'caro', component: SampleCarouselComponent},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
