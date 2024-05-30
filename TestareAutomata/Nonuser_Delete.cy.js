describe('Ticket', () => {
    it('Edit ticket as nonuser', () => {
        cy.visit('https://localhost:7189/Tickets');
        cy.get('[href="/Tickets/Delete/1"]').click();
        cy.get('h1').should('exist');
    })
})