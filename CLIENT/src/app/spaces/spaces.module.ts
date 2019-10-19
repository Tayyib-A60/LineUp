import { StoreModule } from '@ngrx/store';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { TagInputModule } from 'ngx-chips';

import { AddspaceComponent } from './addspace/addspace.component';
import { AdminComponent } from './admin/admin.component';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { SidebarDirective } from '../shared/directives/sidebar.directive';
import { SidebarLinkDirective } from '../shared/directives/sidebarlink.directive';
import { SidebarListDirective } from '../shared/directives/sidebarlist.directive';
import { SidebarAnchorToggleDirective } from '../shared/directives/sidebaranchortoggle.directive';
import { SidebarToggleDirective } from '../shared/directives/sidebartoggle.directive';
import { AppRoutingModule } from '../app-routing.module';
import { ManagespacesComponent } from './managespaces/managespaces.component';
import { ManagereservationsComponent } from './managereservations/managereservations.component';
import { ManagebookingsComponent } from './managebookings/managebookings.component';
import { ManageenquiriesComponent } from './manageenquiries/manageenquiries.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EffectsModule } from '@ngrx/effects';
import { SpaceEffects } from './state/space.effects';
import { reducer } from './state/space.reducers';
import { AddPropsComponent } from './add-props/add-props.component';
import { NgSelectModule } from '@ng-select/ng-select';
 

@NgModule({
  declarations: [
    AddspaceComponent,
    AdminComponent,
    SideBarComponent,
    SidebarDirective,
    SidebarLinkDirective,
    SidebarListDirective,
    SidebarAnchorToggleDirective,
    SidebarToggleDirective,
    ManagespacesComponent,
    ManagereservationsComponent,
    ManagebookingsComponent,
    ManageenquiriesComponent,
    AddPropsComponent
  ],
  imports: [
    AppRoutingModule,
    CommonModule,
    NgxDatatableModule,
    TagInputModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forFeature('spaces', reducer),
    EffectsModule.forFeature([SpaceEffects]),
    NgSelectModule
  ],
  providers: [SidebarDirective]
})
export class SpacesModule { }
