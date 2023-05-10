// eslint-disable-next-line no-undef
module.exports = {
  "env": {
    "browser": true,
    "es2021": true
  },
  "extends": [
    "eslint:recommended",
    "plugin:react/recommended",
    "plugin:react-hooks/recommended",
    "plugin:react/jsx-runtime"
  ],
  "overrides": [
  ],
  "parserOptions": {
    "ecmaVersion": "latest",
    "sourceType": "module"
  },
  "plugins": [
    "react",
    "react-hooks"
  ],
  "rules": {
    "semi": ["warn", "never"],
    "prefer-const": "warn",
    "eqeqeq": "warn",
    "no-unused-vars": "warn",
    "indent": ["warn", 2],
    "no-constant-condition": "warn",
    "no-empty-function": "warn",
    "react/prop-types": "off",
    "no-empty-pattern": "warn",
    "react-hooks/exhaustive-deps": "off",
    "require-jsdoc": ["warn", {
      "require": {
        "FunctionDeclaration": true,
        "MethodDefinition": true,
        "ClassDeclaration": true,
        "ArrowFunctionExpression": true,
        "FunctionExpression": true
      }
    }],
    //"valid-jsdoc": "warn"
  }
}