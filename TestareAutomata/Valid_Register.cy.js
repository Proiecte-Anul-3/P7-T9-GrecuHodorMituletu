describe('Register', () => {
    it('Register ', () => {
        //register
        cy.visit('https://localhost:7189/Identity/Account/Register');
        cy.get('#Input_Email').type('user000@admin.com');
        cy.get('#Input_Password').type('Test123!');
        cy.get('#Input_ConfirmPassword').type('Test123!');
        cy.get('#registerSubmit').click();
        //check error
        cy.get('.validation-summary-errors > ul > li').should('exist');
    })
})
