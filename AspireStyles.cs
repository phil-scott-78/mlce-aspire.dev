namespace Aspire.Dev.Docs;

public static class AspireStyles
{
    public const string Css = """
        /* === Aspire.dev Minimal Styles === */
        /* Only styles that cannot be expressed as Tailwind utility classes */

        /* Prose overrides not covered by MonorailCSS ProseCustomization */
        .prose h2 {
            padding-bottom: 0.5rem;
            border-bottom: 1px solid var(--color-base-200);
        }
        .dark .prose h2 {
            border-bottom-color: var(--color-base-700);
        }
        .prose th {
            background: color-mix(in srgb, var(--color-primary-500) 4%, transparent);
        }
        .dark .prose th {
            background: color-mix(in srgb, var(--color-primary-500) 8%, transparent);
        }


        /* Hero section backgrounds (multi-layer gradients) */
        .hero-section {
            background:
                radial-gradient(circle at 88% 32%, color-mix(in srgb, var(--color-primary-300) 7%, transparent) 0%, transparent 38%),
                linear-gradient(145deg, color-mix(in srgb, var(--color-primary-500) 9%, transparent), color-mix(in srgb, var(--color-primary-300) 5%, transparent), transparent 70%);
        }
        .dark .hero-section {
            background:
                radial-gradient(circle at 88% 32%, color-mix(in srgb, var(--color-primary-300) 8%, transparent) 0%, transparent 38%),
                linear-gradient(145deg, color-mix(in srgb, var(--color-primary-500) 10%, transparent), color-mix(in srgb, var(--color-primary-300) 5%, transparent), transparent 70%);
        }

        /* Hero grid pattern background */
        .hero-grid {
            background-image:
                linear-gradient(color-mix(in srgb, var(--color-primary-500) 6%, transparent) 1px, transparent 1px),
                linear-gradient(90deg, color-mix(in srgb, var(--color-primary-500) 6%, transparent) 1px, transparent 1px);
            background-size: 48px 48px;
            mask-image: radial-gradient(ellipse 80% 60% at 50% 40%, black 30%, transparent 100%);
            -webkit-mask-image: radial-gradient(ellipse 80% 60% at 50% 40%, black 30%, transparent 100%);
        }

        /* Floating icon animation */
        .floating-icon {
            animation: float-icon 7s ease-in-out infinite;
        }

        @keyframes float-icon {
            0%, 100% { transform: translateY(0) rotate(0deg) scale(1); opacity: 0.1; }
            25% { transform: translateY(-6px) rotate(3deg) scale(1.05); opacity: 0.15; }
            50% { transform: translateY(-12px) rotate(-2deg) scale(1.1); opacity: 0.18; }
            75% { transform: translateY(-5px) rotate(1deg) scale(1.02); opacity: 0.12; }
        }

        @media (prefers-reduced-motion: reduce) {
            .floating-icon {
                animation: none;
                opacity: 0.08;
            }
        }
        """;
}
