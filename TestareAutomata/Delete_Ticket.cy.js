describe('Ticket', () => {
    it('Delete ticket logged in as a user', () => {
        //log in as user
        cy.visit('https://localhost:7189/Identity/Account/Login');
        cy.get('#Input_Email').type('user0@mail.com');
        cy.get('#Input_Password').type('Test123!');
        cy.get('#login-submit').click();
        //delete ticket
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('[href="/Tickets/Edit/1"]').click();
        cy.get('h1.text-danger').contains('Access denied');

    })
})