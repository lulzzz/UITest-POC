// //alert("test file");

// // //---------Begin---------------------
// const sum = require('./sum')
// test('properly adds two numbers', () => {
// expect(sum(1,2)).toBe(3)
// })
// // //---------END---------------------

// //---------Begin---------------------

//import puppeteer from 'puppeteer';
import 'regenerator-runtime/runtime'
const puppeteer = require('../node_modules/puppeteer/');


let browser;
const app = 'http://localhost:44386/Tender/AllTendersForVisitor';

test('Validating hotline field', async () => {
  browser = await puppeteer.launch();
  const page = await browser.newPage();
  await page.goto(app);

  const element = await page.$("#hotline19990");
  const text = await page.evaluate(element => element.textContent, element);
  expect(text).toBe(' |  الرقم الموحد :19990');
  await browser.close();
});

// // //---------END---------------------



// //---------Begin---------------------
// const txt = require('./sum')
// test('properly hotline number', () => {
//     expect(txt()).toContain('|  الرقم الموحد :19990')
//     })
// //---------END---------------------
// const fs = require('fs');
// const path = require('path');

// const html = fs.readFileSync(path.resolve('../../Views/Shared/_Layout-Visitor.cshtml'), 'utf8');
// //const html = fs.readFileSync(path.resolve(__dirname, '../index.html'), 'utf8');
// //Src\MOF.Etimad.Monafasat.Web\Views\Shared\_Layout-Visitor.cshtml
// //Src\MOF.Etimad.Monafasat.Web\wwwroot\Etimad-UI\js\sum.test.js
// jest
//     .dontMock('fs');
// debugger
// describe('button', function () {
//     beforeEach(() => {
//         document.documentElement.innerHTML = html.toString();
//     });

//     afterEach(() => {
//         // restore the original func after test
//         jest.resetModules();
//     });

//     expect(document.getElementById('hotline19990').innerHTML).toContain('19990');
//     // it('button exists', function () {
//     //     expect(document.getElementById('disable')).toBeTruthy();
//     // });
// });


// test('properly hotline number', () => {

//     const elm = document.getElementById('hotline19990');
//     expect(elm.innerHTML).toContain('19990');
//     })


// //---------Begin---------------------
// import puppeteer from 'puppeteer';
// let browser;
// const app = 'http://localhost:44386/Tender/AllTendersForVisitor';
// test('proper hotline', async () => {
//   browser = await puppeteer.launch();
//   const page = await browser.newPage();
//   await page.goto(app);
//   await page.waitForSelector('li#hotline19990')
//   let element = await page.$('li#hotline19990')
//   let value = await page.evaluate(el => el.textContent, element)
//   expect(value).toContain('19990np');
//   await browser.close();
// });
// //------------END-----------------
