import { NgModule, Pipe } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatTableModule} from '@angular/material/table';
import { MaterialModule } from './module/material/material.module';
import {MatExpansionModule} from '@angular/material/expansion';

import { Component } from '@angular/core';
import { NavbarComponent } from './navbar/navbar.component';
import { AddProductFormComponent } from './module/cont/add-product-form/add-product-form.component';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        AddProductFormComponent,

        
    ],
    providers: [],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        MaterialModule,
        MatTableModule,
        MatExpansionModule,
        MatSelectModule, MatOptionModule
        
    ]
})

export class AppModule { }
