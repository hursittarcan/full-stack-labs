import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LogoComponent } from './components/logo/logo.component';
import { NewsLetterComponent } from './components/news-letter/news-letter.component';

@NgModule({
  declarations: [
    AppComponent,
    LogoComponent,
    NewsLetterComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
