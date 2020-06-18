import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DeckComponent } from './deck/deck.component';
import { DeckLibraryComponent } from './deck-library/deck-library.component';


const routes: Routes = [
    //{ path: '', component: AppComponent },
    { path: 'deck-library', component: DeckLibraryComponent },
    { path: 'deck/:deckName', component: DeckComponent }
];



@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
