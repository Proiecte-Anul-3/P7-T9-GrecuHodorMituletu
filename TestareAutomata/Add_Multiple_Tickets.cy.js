describe('Ticket', () => {
    it('Add tickets to cart logged in', () => {
        //log in as user
        cy.visit('https://localhost:7189/Identity/Account/Login');
        cy.get('#Input_Email').type('user0@mail.com');
        cy.get('#Input_Password').type('Test123!');
        cy.get('#login-submit').click();
        //add ticket to cart
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('[href="/Carts/AddToCart/1"]').click();
        cy.get('tbody > tr > :nth-child(1)').should('exist');
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('[href="/Carts/AddToCart/2"]').click();
        cy.contains('tbody > :nth-child(2) > :nth-child(1)', '2').should('exist');
    })
})