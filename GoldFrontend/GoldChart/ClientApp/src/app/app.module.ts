import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { jqxChartComponent } from '../../node_modules/jqwidgets-scripts/jqwidgets-ts/angular_jqxchart';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    imports: [
        BrowserModule,
        CommonModule,
        HttpClientModule,
        FormsModule
    ],
    declarations: [
        AppComponent,
        jqxChartComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
