export interface Deck {
    name: string;
    decklist: [
        {
            cardName: string;
            quantity: number;
        }
    ]
    id?: string;
}