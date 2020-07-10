import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { of, from, Observable, Subject, ReplaySubject } from 'rxjs';
import { Deck } from './deck.service';

@Injectable({
    providedIn: 'root'
})
export class DeckLibraryService {


    constructor(private http: HttpClient) {
        this.deckDB = new ReplaySubject(1);
        this.deckLibrary = this.deckDB.asObservable();
    }

    private deckDB: ReplaySubject<Deck[]>;
    deckLibrary: Observable<Deck[]>;

    deck: Deck = { name: 'testing deck', deckList: [{ cardName: 'test values', quantity: 4 }] };

    FetchDecks() {
        //TODO: add type to get call

        this.http.get(
            'https://ng-complete-guide-d3ecb.firebaseio.com/decks.json')
            .pipe(map(responseData => {
                const decksArray = [];
                for (const key in responseData) {
                    if (responseData.hasOwnProperty(key)) {
                        decksArray.push({ ...responseData[key], id: key });
                    }
                }
                return decksArray
            }))
            .subscribe((decksFromDB: Deck[]) => {
                console.log('Fetched Library');
                this.deckDB.next(decksFromDB);
            });
        return this.deckLibrary;
    }

    AddDeck() {
        this.http.post(
            'https://ng-complete-guide-d3ecb.firebaseio.com/decks.json'
            , this.deck).subscribe(deck => {
                console.log(deck);
                this.FetchDecks();
            });
    }

    DeckById(deckId: string): Deck {
        return
    }

}
