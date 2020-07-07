import { Card } from './card.model';

export interface Deck {
    name: string;
    [deckList: number]:
        {
            card: Card;
            quantity: number;
        }
    id?: string;
}