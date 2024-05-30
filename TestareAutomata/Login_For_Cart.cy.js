describe('Cart', () => {
    it('Add ticket to cart not logged in', () => {
        //add ticket
        cy.visit('https://localhost:7189/Tickets');
        cy.get('[href="/Carts/AddToCart/1"]').click();
        cy.get('#login-submit').should('exist');
    })
})