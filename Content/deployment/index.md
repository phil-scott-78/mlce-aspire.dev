---
title: Aspire Deployment
description: Ship your Aspire applications to Azure, Docker, Kubernetes, and beyond — with confidence and consistency.
order: 84
---



## How deployment works

Aspire's deployment model is built on two complementary commands that keep your workflow clean, secure, and extensible.

<QuickStartJourney
  steps={[
    {
      icon: 'seti:powershell',
      title: 'aspire publish',
      description:
        'Transforms your AppHost model into integration-specific artifacts — Docker Compose files, Kubernetes manifests, Bicep templates, and more. Secrets remain as parameterized placeholders.',
      href: '/reference/cli/commands/aspire-publish/',
      label: 'View publish reference',
    },
    {
      icon: 'rocket',
      title: 'aspire deploy',
      description:
        'Resolves parameters, applies configuration, and executes the deployment to your target environment. Works with Azure Container Apps, Kubernetes, and more.',
      href: '/reference/cli/commands/aspire-deploy/',
      label: 'View deploy reference',
    },
    {
      icon: 'setting',
      title: 'Customize your pipeline',
      description:
        'Build custom deployment pipelines that plug into the same model. Use CI/CD variable injection, scripts, or your own toolchain to resolve parameters and apply changes.',
      href: '/deployment/custom-deployments/',
      label: 'Custom pipelines',
    },
  ]}
/>

## Deploy anywhere

Aspire is not locked to a single cloud. Your local dev topology translates directly to production infrastructure through hosting integrations.

## Key concepts

