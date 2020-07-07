import { Component, OnInit, OnDestroy } from '@angular/core';
import { DeckService, } from '../deck.service';
import { Deck } from '../deck.model'
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'mtglo-deck-library',
    templateUrl: './deck-library.component.html',
    styleUrls: ['./deck-library.component.css']
})
export class DeckLibraryComponent implements OnInit, OnDestroy {

    constructor(private deckService: DeckService) { }

    decks: Deck[];
    decksSubscription: Subscription;
    deckFromDB : Deck[]

    ngOnInit(): void {
        this.deckService.FetchDeck();
        this.decksSubscription = this.deckService.decks.subscribe(decks => this.decks = decks);
    }

    ngOnDestroy() {
        this.decksSubscription.unsubscribe();
    }


}
