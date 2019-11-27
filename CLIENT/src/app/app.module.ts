import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { CommonModule } from "@angular/common";
import { NgSelectModule } from '@ng-select/ng-select';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { SpacesModule } from './spaces/spaces.module';
import { HomeComponent } from './home/home.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SignInComponent } from './sign-in/sign-in.component';
import { ListYourSpaceComponent } from './list-your-space/list-your-space.component';
import { FooterComponent } from './footer/footer.component';
import { ProfileComponent } from './profile/profile.component';
import { BookingRequestComponent } from './booking-request/booking-request.component';
import { BookingRequestTwoComponent } from './booking-request-two/booking-request-two.component';
import { SpaceDisplayComponent } from './space-display/space-display.component';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { UserModule } from './state/user.module';
import { UserEffects } from './state/user.effects';
import { BookingModule } from './state/booking/booking.module';
import { BookingEffects } from './state/booking/booking.effects';
import { MapSpaceComponent } from './map-space/map-space.component';
import { SpaceChatComponent } from './space-chat/space-chat.component';
import { SpaceHomeComponent } from './space-home/space-home.component';
import { NewSpaceComponent } from './new-space/new-space.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    ListYourSpaceComponent,
    FooterComponent,
    ProfileComponent,
    BookingRequestComponent,
    BookingRequestTwoComponent,
    SpaceDisplayComponent,
    MapSpaceComponent,
    SpaceChatComponent,
    SpaceHomeComponent,
    NewSpaceComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    NgSelectModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    NgbModule.forRoot(),
    PerfectScrollbarModule,
    SpacesModule,
    UserModule,
    BookingModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([UserEffects, BookingEffects]),
    StoreDevtoolsModule.instrument({
      name: '234 Spaces',
      maxAge: 25,
      logOnly: environment.production
    })
  ],
  exports: [
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
