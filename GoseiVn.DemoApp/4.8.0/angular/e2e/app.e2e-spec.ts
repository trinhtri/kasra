import { DemoAppTemplatePage } from './app.po';

describe('DemoApp App', function() {
  let page: DemoAppTemplatePage;

  beforeEach(() => {
    page = new DemoAppTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
