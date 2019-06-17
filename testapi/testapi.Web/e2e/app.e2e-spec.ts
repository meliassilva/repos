import { PurchaseCardWorkflowPage } from './app.po';

describe('purchase-card-workflow App', () => {
  let page: PurchaseCardWorkflowPage;

  beforeEach(() => {
    page = new PurchaseCardWorkflowPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
