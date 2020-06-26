import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule, NgForm, FormGroup, FormControl, Validators, FormArray } from '@angular/forms'

@Component({
    selector: 'mtglo-deck-editor',
    templateUrl: './deck-editor.component.html',
    styleUrls: ['./deck-editor.component.css']
})
export class DeckEditorComponent implements OnInit {

    genders = ['male', 'female'];
    cardForm: FormGroup;
    editions = ['Alpha', 'Beta', 'Unlimited', 'Revised', 'The Dark', 'Legends'];
    cardsToBeAdded: string[];
    forbiddenCards = ['counterspell'];
    card: {cardName: string, quantity: number};

    constructor() { }

    @Input() public onSubmitCard: (card: {cardName: string, quantity: number}) => void;

    ngOnInit(): void {
        this.cardForm = new FormGroup({
            'cardName': new FormControl('Lightning Bolt', [Validators.required, this.ForbiddenCards.bind(this)]),
            'quantity': new FormControl(4, [Validators.required, Validators.min(1), Validators.max(4)]),
            'edition': new FormControl()
        });

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
}
