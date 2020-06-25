import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DeckComponent } from './deck/deck.component';
import { DeckLibraryComponent } from './deck-library/deck-library.component';
import { CardCollectionComponent } from './card-collection/card-collection.component';


const routes: Routes = [
    {
        path: 'decks',
        component: DeckLibraryComponent,
        children: [
            {path: ':deckName', component: DeckComponent }
        ]
    },
    { path: 'collection', component: CardCollectionComponent}
];



@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
