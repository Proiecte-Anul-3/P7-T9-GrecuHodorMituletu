describe('Login', () => {
    it('Login with valid credentials', () => {
        //login
        cy.visit('https://localhost:7189/Identity/Account/Login');
        cy.get('#Input_Email').type('admin@admin.com');
        cy.get('#Input_Password').type('Test123!');
        cy.get('#login-submit').click();
        cy.get('.display-4').should('exist');
    })
})