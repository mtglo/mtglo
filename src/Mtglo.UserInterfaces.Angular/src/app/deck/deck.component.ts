import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription, of } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';
import { DeckService, Deck } from '../deck.service';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'mtglo-deck',
    templateUrl: './deck.component.html',
    styleUrls: ['./deck.component.css']
})
export class DeckComponent implements OnInit, OnDestroy {

    
    constructor(private route: ActivatedRoute, private deckservice: DeckService) {
    }
    
    // @Input('deck') deck: { deckName: string, deckList: [{ cardName: string, cardQuantity: number }] };
    
    deck: Deck;
    newCard = '';
    subscription: Subscription;
    editingDeck= false;

    public isCollapsed = false;

    ngOnInit(){
        this.subscription = this.route.params.pipe(switchMap( (p: Params) => 
            this.deckservice.decks.pipe(map((decks: Deck[]) => 
                decks.filter(d => d.name === p['deckName'])[0]))
        ))
        .subscribe(deck => {
            this.deck = deck;
            console.log(this.route);
        });
        
    }

    ngOnDestroy(){
        this.subscription.unsubscribe();
    }

}