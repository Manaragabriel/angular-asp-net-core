import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavbarComponent } from './navbar/navbar.component';
import { DataFormatoPipe } from './_helpers/data-formato.pipe';
import {BsDropdownModule,TooltipModule,ModalModule,BsDatepickerModule} from 'ngx-bootstrap'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContatosComponent } from './contatos/contatos.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponentComponent } from './user/registration-component/registration-component.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { EventoEditComponent } from './evento-edit/evento-edit.component';
import { TabsModule } from 'ngx-bootstrap';
import {NgxMaskModule} from 'ngx-mask';
@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    NavbarComponent,
    DataFormatoPipe,
    PalestrantesComponent,
    DashboardComponent,
    ContatosComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponentComponent,
    EventoEditComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ReactiveFormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), 
    TabsModule.forRoot(),
    NgxMaskModule.forRoot(),
    
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
