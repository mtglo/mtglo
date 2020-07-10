import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription, of, Observable } from 'rxjs';
import { switchMap, map, filter } from 'rxjs/operators';
import { DeckService, Deck } from '../deck.service';
import { FormControl } from '@angular/forms';
import { emitWarning } from 'process';
import { DeckLibraryService } from '../deck-library.service';

@Component({
    selector: 'mtglo-deck',
    templateUrl: './deck.component.html',
    styleUrls: ['./deck.component.css']
})
export class DeckComponent implements OnInit, OnDestroy {


    constructor(private deckService: DeckService) {
    }

    // @Input('deck') deck: { deckName: string, deckList: [{ cardName: string, cardQuantity: number }] };

    deck: Deck;
    newCard = '';
    subscription: Subscription;
    editingDeck= false;
    onSavedDeck = function (card: {cardName: string, quantity: number}): void {};
    deckName: string;
    currentDeckSub: Subscription;

    public isCollapsed = false;

    ngOnInit(){
        this.subscription = this.deckService.currentDeck.subscribe(deck => this.deck = deck);
    }

    ngOnDestroy(){
        this.subscription.unsubscribe();
    }

}
