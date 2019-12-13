import { ManagespacesComponent } from './../spaces/managespaces/managespaces.component';
import { MapSpaceComponent } from './../map-space/map-space.component';
import { PaginationComponent } from './../pagination/pagination.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';

@NgModule({
  declarations: [
    PaginationComponent,
    MapSpaceComponent,
    ManagespacesComponent,
    NavbarComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
