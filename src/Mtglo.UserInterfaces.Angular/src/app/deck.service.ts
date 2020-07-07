import { Injectable } from '@angular/core';
import { Observable, Subscriber, from } from 'rxjs';
import { map } from 'rxjs/operators';
import { nextTick } from 'process';
import { HttpClientModule, HttpClient } from '@angular/common/http';

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

    constructor(private http: HttpClient) {
        this.decksDB = [
            { name: 'Burn', deckList: [{ cardName: 'land', quantity: 23 }, { cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Affinity', deckList: [{ cardName: 'ornithopter', quantity: 4 }, { cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Death_and_Taxes', deckList: [{ cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Blue_is_Dumb', deckList: [{ cardName: 'lightning bolt', quantity: 4 }] },
            { name: 'Green_Dudes', deckList: [{ cardName: 'lightning bolt', quantity: 4 }] }
        ];

        this.decks = from([this.decksDB]);
    
    }

    private decksDB: Deck[];
    decks: Observable<Deck[]>;

    GetDeck(deckName: string): Deck {
        return this.decksDB.filter(deck => deck.name === deckName)[0];
    }

    AddCard(deckName: string, card: {cardName: string, quantity: number}) {
        this.GetDeck(deckName).deckList = this.GetDeck(deckName).deckList.concat(card);
    }

    SaveDeck(deck)
    {
        this.http.post(
            'https://ng-complete-guide-d3ecb.firebaseio.com/decks.json'
            , deck).subscribe(deck => {
                console.log(deck)
            });
    }

    FetchDeck()
    {
        //TODO: add type to get call
        this.http.get(
            'https://ng-complete-guide-d3ecb.firebaseio.com/decks.json')
            .pipe(map(responseData => {
                const decksArray =[];
                for (const key in responseData) {
                    if(responseData.hasOwnProperty(key)) {
                        decksArray.push({ ...responseData[key], id: key});
                    }
                }
                return decksArray
            }))
            .subscribe(decksfromDB => {
              console.log(decksfromDB);
            });
    }
}
