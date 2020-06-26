import { Injectable } from '@angular/core';
import { Observable, Subscriber, from } from 'rxjs';
import { nextTick } from 'process';

export type Deck = {
    name: string;
    deckList: CardEntry[];
}

export type CardEntry = {
    cardName: string;
    quantity: number;
}

@Injectable({
    providedIn: 'root'
})
export class DeckService {

    constructor() {
        this.decksDB = [
            { name: 'Burn', deckList: [{ cardName: 'land', quantity: 23 }, { cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Affinity', deckList: [{ cardName: 'ornithopter', quantity: 4 }, { cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Death_and_Taxes', deckList: [{ cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Blue_is_Dumb', deckList: [{ cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Green_Dudes', deckList: [{ cardName: 'lightning bolt', quantity: 4 }] }
        ];

        this.decks = from([this.decksDB])
    }

    private decksDB: Deck[];
    decks: Observable<Deck[]>;

    GetDeck(deckName: string): Deck {
        return this.decksDB.filter(deck => deck.name === deckName)[0];
    }

    AddCard(deckName: string, card: {cardName: string, quantity: number}) {
        this.GetDeck(deckName).deckList = this.GetDeck(deckName).deckList.concat(card);
        console.log(this.GetDeck(deckName).deckList.concat(card));
    }
}
