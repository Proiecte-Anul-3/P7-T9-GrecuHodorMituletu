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
        //remove ticket
        cy.contains('a', 'Remove ticket').click();
        cy.get('div > form > .btn').click();
        
    })
})