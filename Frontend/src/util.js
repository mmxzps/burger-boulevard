import { useApiCacheStore } from '@/stores/apiCacheStore'

export const componentTreeCost = (componentTree) => {
  const components = useApiCacheStore().components

  return components
}

export const diff = (componentTree) =>
  useApiCacheStore().components
  .find(({ id }) => id == componentTree.componentId).childPolicies
  .reduce((diffs, policy) => {
    const childrenByPolicy = componentTree.children.filter(o => o.componentId == policy.child.id)
    const added = childrenByPolicy.length - policy.default

    const changes = Array(Math.abs(added))
    .fill(this.components.find(({ id }) => id == policy.child.id))

    if (added > 0)
      diffs.added.push(...changes)
    else if (added < 0)
      diffs.removed.push(...changes)

    return diffs
  }, {
    added: [],
    removed: []
  })
