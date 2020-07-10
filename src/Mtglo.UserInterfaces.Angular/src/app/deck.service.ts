import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { DeckLibraryService } from './deck-library.service';

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

    constructor(private route: ActivatedRoute, private http: HttpClient, private deckLibraryService: DeckLibraryService) {
                this.selectedDeck = {name: 'default value', deckList: [{cardName: 'test', quantity: 3}]};
        this.currentlySelectedDeck = new ReplaySubject(1);
        this.currentDeck = this.currentlySelectedDeck.asObservable();

    }

    private currentlySelectedDeck: ReplaySubject<Deck>;
    public currentDeck: Observable<Deck>;
    retrievedDecks: Deck[];

    private selectedDeck: Deck;

    SelectDeck(deckId: string)
    {
        this.deckLibraryService.deckLibrary.subscribe(decks =>  this.selectedDeck = decks.find(deck => deck.name === deckId));
        console.log(`New deck selected: ${deckId}`);
        console.log(this.selectedDeck);
        this.currentlySelectedDeck.next(this.selectedDeck);
    }

    UpdateDeck(deck: Deck) {
        //this.post
    }

    SaveDeck(deck) {
        this.http.post(
            'https://ng-complete-guide-d3ecb.firebaseio.com/decks.json'
            , deck).subscribe(deck => {
                console.log(deck)
            });
    }

    FetchDeck() {
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
            .subscribe(decksfromDB => {
                this.retrievedDecks = decksfromDB;
                console.log('Retrieved Decks');
            });
    }
}
