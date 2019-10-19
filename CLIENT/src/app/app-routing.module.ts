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


const routes: Routes = [
  { path: 'admin', component: AdminComponent, children: [
    { path: 'add-space', component: AddspaceComponent },
    { path: 'manage-space', component: ManagespacesComponent },
    { path: 'manage-reservations', component: ManagereservationsComponent },
    { path: 'manage-bookings', component: ManagebookingsComponent },
    { path: 'manage-enquiries', component: ManageenquiriesComponent }
  ]},
  { path: '', component: HomeComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'list-your-space', component: ListYourSpaceComponent },
  { path: 'profile', component: ProfileComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
