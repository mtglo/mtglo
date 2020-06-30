import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DeckComponent } from './deck/deck.component';
import { DecklistComponent } from './decklist/decklist.component';
import { CardComponent } from './card/card.component';
import { DeckLibraryComponent } from './deck-library/deck-library.component';
import { HorizontalNavigationBarComponent } from './horizontal-navigation-bar/horizontal-navigation-bar.component';
import { CardCollectionComponent } from './card-collection/card-collection.component';
import { DeckEditorComponent } from './deck-editor/deck-editor.component';

@NgModule({
  declarations: [
    AppComponent,
    DeckComponent,
    DecklistComponent,
    CardComponent,
    DeckLibraryComponent,
    HorizontalNavigationBarComponent,
    CardCollectionComponent,
    DeckEditorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
