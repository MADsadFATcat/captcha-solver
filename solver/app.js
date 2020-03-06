const { createWorker, PSM } = require('tesseract.js');
const fs = require('fs');
const image = require('./image');

const path = process.argv[2] || '../captchas/';

const worker = createWorker();

(async () => {
  await worker.load();
  await worker.loadLanguage('rus');
  await worker.initialize('rus');
  await worker.setParameters({
    tessedit_char_whitelist: '0123456789',
    tessedit_pageseg_mode: PSM.SINGLE_WORD,
  });

  const files = fs.readdirSync(path);

  for (let file of files) {
    const data = await image.parse(path + file);
    const ans = file.split('.')[0];
    let result = await worker.recognize(data);


    result = result.data.text.replace(/[\D]/gmi, '');
    if (result === ans) {
      fs.appendFileSync('good.txt', result + '|' + ans + '\n');
      console.log('good', result, '|', ans);
    } else {
      fs.appendFileSync('bad.txt', result + '|' + ans + '\n');
      console.log('bad', result, '|', ans);
    }
  }

  await worker.terminate();
})();
