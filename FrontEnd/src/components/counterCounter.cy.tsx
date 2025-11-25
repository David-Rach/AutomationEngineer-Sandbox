import { Counter } from './counter'


describe('Counter', () => {

  
  /// Ensure the Counter component mounts successfully
  it('component mounts', () => {
    cy.mount(<Counter />)
  })


  ///Ensure increment and decrement buttons work correctly
  // - The count starts at 0
  // - Clicking "Increment" increases the count by 1
  // - Clicking "Decrement" decreases the count by 1
  it('increments and decrements', () => {
    throw new Error('Not implemented yet')
  })


  // Ensure the counter does not go below 0
  it('should not go below 0 (candidate should fix this)', () => {
    throw new Error('Not implemented yet')
  })
})