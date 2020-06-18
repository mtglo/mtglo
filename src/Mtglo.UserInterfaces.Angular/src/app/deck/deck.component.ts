import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'mtglo-deck',
    templateUrl: './deck.component.html',
    styleUrls: ['./deck.component.css']
})
export class DeckComponent implements OnInit {

    deck: {deckName: string};
    newCard = '';
    currentDeckList: { deckName: string, deckList: string;}

    constructor(private route: ActivatedRoute) {
    }

    // @Input('deck') deck: { deckName: string, deckList: [{ cardName: string, cardQuantity: number }] };

    listOfDecks = [{ deckName: 'Burn', deckList: 'cards 1'}, { deckName: 'Affinity', deckList: 'cards 2' }, { deckName: 'Death_and_Taxes', deckList: 'cards 3' }, { deckName: 'Blue_is_Dumb', deckList: 'cards 4' }, { deckName: 'Green_Dudes', deckList: 'cards 5' }];

    public isCollapsed = false;

    ngOnInit(){
        this.deck = {
            deckName: this.route.snapshot.params['deckName']
        };
        this.currentDeckList = this.listOfDecks.find(element => element.deckName == this.deck.deckName);
    }

}