module.exports = {
  transpileDependencies: true,
  configureWebpack: {
    module: {
      rules: [
        {
          test: /\.bpmnlintrc$/,
          use: 'json-loader',
        },
        {
          test: /\.bpmn$/,
          use: 'xml-loader',
        },
      ],
    },
  },
};
