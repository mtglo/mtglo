import { Component } from '@angular/core';

import { DeckComponent } from './deck/deck.component';
import { DeckLibraryComponent } from './deck-library/deck-library.component'


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'mtglo';
    deckList = [{ cardName: 'Land', cardQuantity: 23 }, { cardName: 'Lightning Bolt', cardQuantity: 4 }, { cardName: 'Carmaggedon', cardQuantity: 4 }, { cardName: 'Demonic Puter', cardQuantity: 4 }]
    decks = [{ deckName: 'Burn', deckList: this.deckList }, { deckName: 'Affinity', deckList: this.deckList }];
}
