module.exports = {
  transpileDependencies: true,
  configureWebpack: {
    module: {
      rules: [
        {
          test: /\.bpmn$/,
          use: 'xml-loader',
        },
      ],
    },
  },
};
