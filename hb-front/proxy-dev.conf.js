const defaultTarget = 'http://localhost:5243';

module.exports = [
  {
    context: ['/api/**'],
    target: defaultTarget,
    changeOrigin: true
  }
]
