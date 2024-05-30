describe('Login', () => {
    it('Login with wrong credentials(check error message)', () => {
        
        cy.visit('https://localhost:7189/Identity/Account/Login');
        cy.get('#Input_Email').type('admin000@mail.com');
        cy.get('#Input_Password').type('abcd');
        cy.get('#login-submit').click();
        cy.get('.validation-summary-errors > ul > li').should('exist');
    })
})