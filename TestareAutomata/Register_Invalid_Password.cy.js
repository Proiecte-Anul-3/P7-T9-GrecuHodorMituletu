describe('Register', () => {
    it('Register ', () => {
        //register
        cy.visit('https://localhost:7189/Identity/Account/Register');
        cy.get('#Input_Email').type('user10@admin.com');
        cy.get('#Input_Password').type('a');

        //check error
        cy.get('#Input_ConfirmPassword').click();
        cy.get('#Input_Password-error').should('exist');

    })
})
