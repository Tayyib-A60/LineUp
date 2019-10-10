import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './spaces/admin/admin.component';
import { HomeComponent } from './home/home.component';
import { AddspaceComponent } from './spaces/addspace/addspace.component';
import { ManagespacesComponent } from './spaces/managespaces/managespaces.component';
import { ManagereservationsComponent } from './spaces/managereservations/managereservations.component';
import { ManagebookingsComponent } from './spaces/managebookings/managebookings.component';
import { ManageenquiriesComponent } from './spaces/manageenquiries/manageenquiries.component';


const routes: Routes = [
  { path: 'admin', component: AdminComponent, children: [
    { path: 'add-space', component: AddspaceComponent },
    { path: 'manage-space', component: ManagespacesComponent },
    { path: 'manage-reservations', component: ManagereservationsComponent },
    { path: 'manage-bookings', component: ManagebookingsComponent },
    { path: 'manage-enquiries', component: ManageenquiriesComponent }
  ]},
  { path: '', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
