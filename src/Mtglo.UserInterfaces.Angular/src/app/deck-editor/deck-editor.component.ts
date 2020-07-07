import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormsModule, NgForm, FormGroup, FormControl, Validators, FormArray } from '@angular/forms'
import { from, Observable, of, fromEvent, observable } from 'rxjs';
import { stringify } from 'querystring';


@Component({
    selector: 'mtglo-deck-editor',
    templateUrl: './deck-editor.component.html',
    styleUrls: ['./deck-editor.component.css']
})
export class DeckEditorComponent implements OnInit {

    genders = ['male', 'female'];
    cardForm: FormGroup;
    editions = ['Alpha', 'Beta', 'Unlimited', 'Revised', 'The Dark', 'Legends'];
    cardsToBeAdded: [{ cardName: string, quantity: number}];
    forbiddenCards = ['counterspell'];
    card: {cardName: string, quantity: number};
    newCard: {cardName: string, quantity: number};   


    constructor() { }

    @Input() public onSubmitCard: (card: {cardName: string, quantity: number}) => void;

    ngOnInit(): void {
        this.cardForm = new FormGroup({
            'cardName': new FormControl('Lightning Bolt', [Validators.required, this.ForbiddenCards.bind(this)]),
            'quantity': new FormControl(4, [Validators.required, Validators.min(1), Validators.max(4)]),
            'edition': new FormControl()
        });
        var button = document.querySelector('addCard');
        const myobs = of(this.newCard);
        const myobserver = {
            next: x => this.cardsToBeAdded.concat(x)
        }
        var cardsubscriber = myobs.subscribe(myobserver);

    }

    OnAddCard() {
        this.newCard.cardName=this.cardForm.value['cardName'];
        this.newCard.quantity=this.cardForm.value['quantity'];

        this.cardForm.reset();
    }

    OnSubmit() {
        this.card = {cardName: this.cardForm.value['cardName'], quantity: this.cardForm.value['quantity']};
        this.onSubmitCard(this.card);
    }

    ForbiddenCards(control: FormControl): {[s: string]: boolean} {
        if (!this.forbiddenCards.indexOf(control.value)) {
            return {'cardIsForbidden': true};
        }
        return null;
    }
    
    OnDestroy() {

    }
}
