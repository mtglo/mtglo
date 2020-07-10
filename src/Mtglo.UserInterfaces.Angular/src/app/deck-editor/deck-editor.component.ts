import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormsModule, NgForm, FormGroup, FormControl, Validators, FormArray } from '@angular/forms'
import { from, Observable, of, fromEvent, observable, Subscriber, Subscription } from 'rxjs';
import { stringify } from 'querystring';
import { DeckService, Deck } from '../deck.service';


@Component({
    selector: 'mtglo-deck-editor',
    templateUrl: './deck-editor.component.html',
    styleUrls: ['./deck-editor.component.css']
})
export class DeckEditorComponent implements OnInit {

    genders = ['male', 'female'];
    cardForm: FormGroup;
    editions = ['Alpha', 'Beta', 'Unlimited', 'Revised', 'The Dark', 'Legends'];
    cardsToBeAdded = [{cardName: 'test value', quantity: 3}];
    forbiddenCards = ['counterspell'];
    card: {cardName: string, quantity: number};
    newCard: {cardName: string, quantity: number};
    cardSubscriber: Subscription;
    currentDeck: Deck;
    deckSub: Subscription;

    constructor(private deckService: DeckService) { }

    @Input() public onSubmitCard: (card: {cardName: string, quantity: number}) => void;
    @Output()

    ngOnInit(): void {
        this.deckSub = this.deckService.currentDeck.subscribe(deck => this.currentDeck = deck);
        this.cardForm = new FormGroup({
            'cardName': new FormControl('Lightning Bolt', [Validators.required, this.ForbiddenCards.bind(this)]),
            'quantity': new FormControl(4, [Validators.required, Validators.min(1), Validators.max(4)]),
            'edition': new FormControl()
        });
        var button = document.querySelector('addCard');

    }

    OnAddCard() {
        this.currentDeck.deckList.push({cardName: this.cardForm.value['cardName'], quantity: this.cardForm.value['quantity']});
        console.log('Adding a new card!!!')
    }

    OnSubmit() {
        this.card = {cardName: this.cardForm.value['cardName'], quantity: this.cardForm.value['quantity']};
        //this.onSubmitCard(this.card);
    }

    ForbiddenCards(control: FormControl): {[s: string]: boolean} {
        if (!this.forbiddenCards.indexOf(control.value)) {
            return {'cardIsForbidden': true};
        }
        return null;
    }

    OnDestroy() {
        this.deckSub.unsubscribe();
    }
}
