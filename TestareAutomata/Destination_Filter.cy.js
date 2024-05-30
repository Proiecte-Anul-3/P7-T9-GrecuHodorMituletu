describe('Tickets', () => {
    it('Verify destination filter', () => {
        //log in
        cy.visit('https://localhost:7189/Identity/Account/Login');
        cy.get('#Input_Email').type('user0@mail.com');
        cy.get('#Input_Password').type('Test123!');
        cy.get('#login-submit').click();
        //filter by destination
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('[name="DestinationString"]').type('Cluj');
        cy.get('p > [type="submit"]').click();
        cy.get('tbody > tr > :nth-child(3)').contains('Cluj');
    })
})