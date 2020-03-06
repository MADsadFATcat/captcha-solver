const { createCanvas, loadImage } = require('canvas');

module.exports = {
  async parse(img) {
    const canvas = createCanvas(80, 30);
    const ctx = canvas.getContext('2d');
    const image = await loadImage(img);
    ctx.drawImage(image, 0, 0);
    let data = ctx.getImageData(0, 0, 80, 30);
    data = this.polarize(data);
    ctx.putImageData(data, 0, 0);
    return canvas.toBuffer();
  },
  polarize(imgData) {
    for (let x = 0; x < imgData.width; x++) {
      for (let y = 0; y < imgData.height; y++) {
        const i = x * 4 + y * 4 * imgData.width;
        const pix = { r: imgData.data[i], g: imgData.data[i + 1], b: imgData.data[i + 2] };

        if (this.isBack(pix)) {
          imgData.data[i] = 255;
          imgData.data[i + 1] = 255;
          imgData.data[i + 2] = 255;
          imgData.data[i + 3] = 255;
        } else {
          imgData.data[i] = 0;
          imgData.data[i + 1] = 0;
          imgData.data[i + 2] = 0;
          imgData.data[i + 3] = 255;
        }
      }
    }

    return imgData;
  },
  isBack(x) {
    return (x.r + x.g + x.b) < 306;
  },
};
