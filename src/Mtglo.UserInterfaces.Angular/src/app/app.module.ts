import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DeckComponent } from './deck/deck.component';
import { DecklistComponent } from './decklist/decklist.component';
import { CardComponent } from './card/card.component';
import { DeckLibraryComponent } from './deck-library/deck-library.component';
import { HorizontalNavigationBarComponent } from './horizontal-navigation-bar/horizontal-navigation-bar.component';
import { CardCollectionComponent } from './card-collection/card-collection.component';

@NgModule({
  declarations: [
    AppComponent,
    DeckComponent,
    DecklistComponent,
    CardComponent,
    DeckLibraryComponent,
    HorizontalNavigationBarComponent,
    CardCollectionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
